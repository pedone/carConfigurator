'use strict';

class ConfigurationOverviewViewModel {
    /** @param {HandlerOptions} options */
    constructor(options) {
        let self = this;
        /** @type {string} */
        this.configurationDescription = ko.observable('');
        /** @type {HandlerOptions} */
        this.options = options;

        this.placeOrder = function () {
            $.ajax({
                type: 'POST',
                url: '/ConfigurationOverview/PlaceOrder',
                data: { __RequestVerificationToken: self.options.antiForgeryToken, description: self.configurationDescription() },
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'text'
            }).done(
                function (data) {
                    console.debug('order successfully placed');
                    if (self.options.placeOrderSuccess) {
                        self.options.placeOrderSuccess(data);
                    }
                })
                .fail(function (error) {
                    console.error('failed to place order: ' + error.responseText + ' (' + error.statusText + ')');
                    console.debug(JSON.stringify(error));
                    if (self.options.placeOrderFailure) {
                        self.options.placeOrderFailure();
                    }
                });
        };
    }
}

/**
 * @typedef {Object} HandlerOptions
 * @property {string} antiForgeryToken
 * @property {Function|null} placeOrderSuccess
 * @property {Function|null} placeOrderFailure
 */
'use strict';

class ViewModel {
    /** @param {ViewModelData|NameViewModelData|AccessoryViewModelData|RimViewModelData} data */
    constructor(data) {
        /** @type {number} */
        this.id = data.Id;
        /** @type {number} */
        this.price = data.Price;
        /** @type {boolean} */
        this.isSelected = ko.observable(data.IsSelected);
        /** @type {number|null} */
        this.name = data.Name;
        /** @type {number|null} */
        this.size = data.Size;
        /** @type {number|null} */
        this.category = data.Category;
    }
}

/**
 * @property {Object.<ViewModel>} _engineSettingsById
 * @property {Object.<ViewModel>} _rimsById
 * @property {Object.<ViewModel>} _paintById
 * @property {Object.<ViewModel>} accessoriesById
 * @property {Array.<ViewModel>} selectedAccessories
 */
class ConfigurationViewModel {
    /**
     * @param {ConfigurationData} data
     */
    constructor(data) {
        Object.defineProperties(this, {
            _engineSettingsById: {
                value: this._toViewModelDictionary(data.EngineSettings),
                writable: false,
                enumerable: false
            },
            _rimsById: {
                value: this._toViewModelDictionary(data.Rims),
                writable: false,
                enumerable: false
            },
            _paintById: {
                value: this._toViewModelDictionary(data.Paints),
                writable: false,
                enumerable: false
            },
            accessoriesById: {
                value: this._toViewModelDictionary(data.Accessories),
                writable: false,
                enumerable: true
            }
        });

        /**
         * koObservable
         * @type {string}
         */
        this.selectedPaintId = ko.observable(this._getInitialSelectedId(data.Paints));
        /** @type {string} */
        this._initialPaintId = this.selectedPaintId();

        /**
         * koObservable
         * @type {string}
         */
        this.selectedRimsId = ko.observable(this._getInitialSelectedId(data.Rims));
        /** @type {string} */
        this._initialRimsId = this.selectedRimsId();

        /**
         * koObservable
         * @type {Array.<ViewModel>}
         */
        this.selectedAccessories = ko.observableArray(_.values(this.accessoriesById)).extend({ filterSelected: null });
        /** @type {Array.<number>} */
        this._initialAccessoryIds = this.selectedAccessoryIds;

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedEngineSettings = ko.computed(() => {
            let values = _.values(this._engineSettingsById);
            // only one setting is supposed to be selected
            return _.first(_.filter(values, (cur) => cur.isSelected()));
        });
        /** @type {ViewModel} */
        this._initialEngineSettings = this.selectedEngineSettings();

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedPaint = ko.computed(() => this._paintById[this.selectedPaintId()]);

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedRims = ko.computed(() => this._rimsById[this.selectedRimsId()]);

        /**
         * Calculates the combined price of everything but the engine
         * koComputed
         * @type {number}
         */
        this.extrasPrice = ko.computed(() => {
            /** @type {number} */
            let accessoriesPrice = _.reduce(this.selectedAccessories(), (memo, cur) => memo + cur.price, 0);
            return accessoriesPrice + (this.selectedPaint() && this.selectedPaint().price) + (this.selectedRims() && this.selectedRims().price);
        });

        /**
         * Just the engine price
         * koComputed
         * @type {number}
         */
        this.basePrice = ko.computed(() => {
            return (this.selectedEngineSettings() && this.selectedEngineSettings().price) || 0;
        });

        /**
         * The base price combined with the extras price
         * koComputed
         * @type {number}
         */
        this.fullPrice = ko.computed(() => {
            return this.basePrice() + this.extrasPrice();
        });
    }

    /**
     * @param {Array.<ViewModelData>} items
     * @returns {string}
     * @private
     */
    _getInitialSelectedId(items) {
        if (!items || items.length === 0) {
            return -1;
        }

        /** @type {ViewModelData} */
        let selectedItem = _.find(items, (cur) => cur.IsSelected) || items[0];
        return selectedItem.Id.toString();
    }

    /**
     * @param {Array.<ViewModelData>} items
     * @returns {Object.<ViewModel>}
     * @private
     */
    _toViewModelDictionary(items) {
        /** @type {Object.<ViewModel>} */
        var result = {};

        _.each(items, (cur) => { result[cur.Id] = new ViewModel(cur); });
        return result;
    }

    /** @returns {Array.<number>} */
    get selectedAccessoryIds() {
        return _.map(this.selectedAccessories(), (cur) => cur.id);
    }

    /**
     * @param {Array.<ViewModel>} accessories
     * @param {string} category
     */
    countCategory(accessories, category) {
        return _.filter(accessories, (cur) => cur.category === category).length;
    }

    /** @param {number} settingsId */
    selectEngineSettings(settingsId) {
        //deselect all other settings, because deselection doesn't work with binding
        _.each(this._engineSettingsById, (cur) => { cur.isSelected(cur.id === settingsId) });
    }

    /** @param {string} id */
    selectPaint(id) {
        this.selectedPaintId(id);
    }

    /** @param {string} id */
    selectAccessory(id) {
        /** @type {boolean} */
        let isSelected = this.accessoriesById[id].isSelected();
        this.accessoriesById[id].isSelected(!isSelected);
    }

    /** @param {string} antiForgeryToken */
    saveChanges(antiForgeryToken) {
        let selectedAccessoryIds = this.selectedAccessoryIds;

        //check for changes
        let accessoriesChanged = (this._initialAccessoryIds.length !== selectedAccessoryIds.length || _.difference(this._initialAccessoryIds, this.selectedAccessoryIds).length > 0);
        let engineSettingsChanged = this._initialEngineSettings != this.selectedEngineSettings();
        let paintChanged = this._initialPaintId != this.selectedPaintId();
        let rimsChanged = this._initialRimsId != this.selectedRimsId();

        if (!accessoriesChanged && !engineSettingsChanged && !paintChanged && !rimsChanged) {
            console.debug('no configuration changes');
            return;
        }

        // package changes
        let changedData = { __RequestVerificationToken: antiForgeryToken };
        if (paintChanged) {
            changedData.paintId = viewModel.selectedPaintId();
        }
        if (rimsChanged) {
            changedData.rimId = viewModel.selectedRimsId();
        }
        if (engineSettingsChanged) {
            changedData.engineSettingsId = viewModel.selectedEngineSettings().id;
        }
        if (accessoriesChanged) {
            changedData.accessoryIds = selectedAccessoryIds;
        }

        console.debug('saving configuration changes');

        //send changes
        $.ajax({
            //make sure the changes are saved before the next page is loaded
            async: false,
            type: 'POST',
            url: '/Configuration/UpdateActiveConfiguration',
            data: changedData,
            contentType: 'application/x-www-form-urlencoded'
        }).fail((error) => {
            console.error('failed to save configuration changes: ' + error.responseText + ' (' + error.statusText + ')');
            console.debug(JSON.stringify(error));
            //alert('something went wrong. see console for details');
        });
    }
}

/**
 * @typedef {Object} ViewModelData
 * @property {number} Id
 * @property {number} Price
 * @property {boolean} IsSelected
 */

/**
 * @typedef {ViewModelData} NameViewModelData
 * @property {string} Name
 */

/**
 * @typedef {ViewModelData} RimViewModelData
 * @property {number} Size
 */

/**
 * @typedef {NameViewModelData} AccessoryViewModelData
 * @property {number} Category
 */

/**
 * @typedef {Object} ConfigurationData
 * @property {Array.<ViewModelData>} EngineSettings
 * @property {Array.<ViewModelData>} Accessories
 * @property {Array.<ViewModelData>} Paints
 * @property {Array.<ViewModelData>} Rims
 */

'use strict';

/**
 * @param {string} url
 * @param {string} id
 * @param {string} antiForgeryToken
 * @param {Object} [additionalData]
 * @returns {jqXHR}
 */
function saveViewModel(url, id, antiForgeryToken, additionalData) {
    let i;
    /** @type {Object} */
    let dataObject = {
        __RequestVerificationToken: antiForgeryToken
    };

    if (id) {
        dataObject.id = id;
    }
    if (additionalData) {
        for (i in additionalData) {
            if (additionalData.hasOwnProperty(i)) {
                dataObject[i] = additionalData[i];
            }
        }
    }
    return $.ajax({
        type: 'POST',
        url: url,
        data: dataObject,
        contentType: 'application/x-www-form-urlencoded'
    });
}

/**
 * @param {Document} document
 * @returns {string}
 */
function getAntiForgeryToken(document) {
    return document.find('[name="__RequestVerificationToken"]').val();
}

function formatCurrency(amount) {
    return amount.toLocaleString() + ' EUR';
}
'use strict';

/** @param {KnockoutObservableArrayStatic<ViewModel>} target */
ko.extenders.filterSelected = function (target) {
    /** @type {number} */
    let i;
    /** @type {Array.<ViewModel>} */
    let data = target();
    /** @type {KnockoutObservableArrayStatic<ViewModel>} */
    let result = ko.observableArray(_.filter(data, (cur) => cur.isSelected()));
    /** @type {Function} */
    var subscribeFactory = function (index) {
        return function (value) {
            if (value) { 
                result.push(data[index]);
            } else {
                result.remove(data[index]);
            }
        }
    };

    for (i = 0; i < data.length; i += 1) {
        //capture the current index with a function factory
        data[i].isSelected.subscribe(subscribeFactory(i));
    }
    return result;
};

'use strict';

class ModelSelectionViewModel {
    constructor() {
        /**
         * koObservable
         * @type {string}
         */
        this.curModelFilter = ko.observable('');
    }

    /**
     * @param {string} filter
     */
    filterModels(filter) {
        if (filter !== this.curModelFilter()) {
            this.curModelFilter(filter);
        }
    }
}
'use strict';

class OrderItemViewModel {
    /** @param {OrderData} data */
    constructor(data) {
        /** @type {number} */
        this.id = data.Id;
        /** @type {number} */
        this.basePrice = data.BasePrice;
        /** @type {number} */
        this.extrasPrice = data.ExtrasPrice;
        /** @type {number} */
        this.price = data.Price;
        /** @type {string} */
        this.description = data.Description;
        /** @type {string} */
        this.dateTime = data.DateTime;
        /** @type {string} */
        this.model = data.Model;
        /** @type {string} */
        this.guid = data.Guid;
        /** @type {string} */
        this.linkUrl = data.LinkUrl;
    }
}

class OrderListViewModel {
    /**
     * @param {Array.<OrderData>} data
     * @param {number} pageCount
     */
    constructor(data, pageCount) {
        let self = this;
        /** @type {KnockoutObservableArrayStatic} */
        this.orders = ko.observableArray(_.map(data, (cur) => new OrderItemViewModel(cur)));
        /**
         * koObservableArray
         * @type {Array.<number>}
         */
        this.pages = ko.observableArray([]);
        _.times(pageCount, (index) => this.pages().push(index + 1));
        /**
         * koObservable
         * @type {number}
         */
        this.currentPageIndex = ko.observable(0);

        /**
         * Notes: this method has to be placed within the constructor, because self.orders is not returning the
         * KnockoutObservableArrayStatic object outside the constructor, but the function that knockout is generating.
         * @param {number} itemId
         * @param {JQueryStatic} document
         */
        this.deleteItem = function (itemId, document) {
            var item = _.find(self.orders(), (cur) => cur.id == itemId);
            if (!item) {
                console.error('order ' + itemId + ' not found');
                return;
            }

            /** @type {string} */
            let antiForgeryToken = getAntiForgeryToken(document);
            deleteItemAjax(itemId, antiForgeryToken)
                .done(
                    /** @param {{NewPageCount: number, NewItem: OrderData }} data */
                    function (data) {
                        console.debug('removing order ' + itemId + ' from view');
                        self.orders.remove(item);
                        if (data.NewItem) {
                            //insert the item that has moved up to the current page
                            self.orders.push(new OrderItemViewModel(data.NewItem));
                        }
                        if (self.pages().length !== data.NewPageCount) {
                            console.debug('page count changed from ' + self.pages().length + ' to ' + data.NewPageCount);
                            //update the max page count
                            self.pages.pop();

                            // if the current page is larger than the max pages, gotta load the previous page
                            /** @type {number} */
                            let curPageNumber = self.currentPageIndex() + 1;
                            if (curPageNumber > self.pages().length) {
                                self.loadPage(curPageNumber - 1);
                            }
                        }
                    })
                .fail(function (error) {
                    console.error('failed to delete order: ' + error.responseText + ' (' + error.statusText + ')');
                    console.debug(JSON.stringify(error));
                    alert('order ' + item.name + ' could not be removed');
                });
        };

        /**
         * @param {number} id
         * @param {string} antiForgeryToken
         * @returns {jqXHR}
         */
        function deleteItemAjax(id, antiForgeryToken) {
            return $.ajax({
                type: 'POST',
                url: '/OrderList/delete',
                data: {
                    __RequestVerificationToken: antiForgeryToken,
                    id: id,
                    pageIndex: self.currentPageIndex()
                },
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json'
            });
        }

        /** @param {number} number */
        this.loadPage = function (number) {
            /** @type {number} */
            let targetIndex = number - 1;

            if (targetIndex === self.currentPageIndex()) {
                return;
            }

            $.ajax({
                //make sure the data is saved before the next page is loaded
                async: false,
                type: 'GET',
                url: '/OrderList/LoadPage',
                data: { pageIndex: targetIndex },
                dataType: 'json'
            }).done(function (data) {
                console.debug('order list for page index ' + targetIndex + ' successfully retrieved');
                self.currentPageIndex(targetIndex);
                self.orders(_.map(data, (cur) => new OrderItemViewModel(cur)));
            }).fail(function (error) {
                console.error('failed to load page: ' + error.responseText + ' (' + error.statusText + ')');
                console.debug(JSON.stringify(error));
            });
        };
    }
}

/**
 * @typedef {Object} OrderData
 * @property {number} Id
 * @property {number} ExtrasPrice
 * @property {number} BasePrice
 * @property {number} Price
 * @property {string} Description
 * @property {string} DateTime
 * @property {string} Model
 * @property {string} Guid
 * @property {string} LinkUrl
 */
export const dog = {
    talk: function () {
        console.log('bark');
    }
}

export const cat = {
    talk: function () {
        console.log('meow');
    }
}

export const cow = {
    talk: function () {
        console.log('muuh');
        animalTest2();
    }
}

function animalTest2() {
    console.log('animalTest');
}

function animalTest() {
    console.log('animalTest');
}

console.log('animals loaded');
'use strict';

import { dog, cat } from './animals.js'

function test() {
    cat.talk();
    dog.talk();
}

export function test2() {
    console.log('main test2');
}

console.log('loaded');