'use strict';
var ViewModel = /** @class */ (function () {
    /** @param {ViewModelData|NameViewModelData|AccessoryViewModelData|RimViewModelData} data */
    function ViewModel(data) {
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
    return ViewModel;
}());
/**
 * @property {Object.<ViewModel>} _engineSettingsById
 * @property {Object.<ViewModel>} _rimsById
 * @property {Object.<ViewModel>} _paintById
 * @property {Object.<ViewModel>} accessoriesById
 * @property {Array.<ViewModel>} selectedAccessories
 */
var ConfigurationViewModel = /** @class */ (function () {
    /**
     * @param {ConfigurationData} data
     */
    function ConfigurationViewModel(data) {
        var _this = this;
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
        this.selectedEngineSettings = ko.computed(function () {
            var values = _.values(_this._engineSettingsById);
            // only one setting is supposed to be selected
            return _.first(_.filter(values, function (cur) { return cur.isSelected(); }));
        });
        /** @type {ViewModel} */
        this._initialEngineSettings = this.selectedEngineSettings();
        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedPaint = ko.computed(function () { return _this._paintById[_this.selectedPaintId()]; });
        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedRims = ko.computed(function () { return _this._rimsById[_this.selectedRimsId()]; });
        /**
         * Calculates the combined price of everything but the engine
         * koComputed
         * @type {number}
         */
        this.extrasPrice = ko.computed(function () {
            /** @type {number} */
            var accessoriesPrice = _.reduce(_this.selectedAccessories(), function (memo, cur) { return memo + cur.price; }, 0);
            return accessoriesPrice + (_this.selectedPaint() && _this.selectedPaint().price) + (_this.selectedRims() && _this.selectedRims().price);
        });
        /**
         * Just the engine price
         * koComputed
         * @type {number}
         */
        this.basePrice = ko.computed(function () {
            return (_this.selectedEngineSettings() && _this.selectedEngineSettings().price) || 0;
        });
        /**
         * The base price combined with the extras price
         * koComputed
         * @type {number}
         */
        this.fullPrice = ko.computed(function () {
            return _this.basePrice() + _this.extrasPrice();
        });
    }
    /**
     * @param {Array.<ViewModelData>} items
     * @returns {string}
     * @private
     */
    ConfigurationViewModel.prototype._getInitialSelectedId = function (items) {
        if (!items || items.length === 0) {
            return -1;
        }
        /** @type {ViewModelData} */
        var selectedItem = _.find(items, function (cur) { return cur.IsSelected; }) || items[0];
        return selectedItem.Id.toString();
    };
    /**
     * @param {Array.<ViewModelData>} items
     * @returns {Object.<ViewModel>}
     * @private
     */
    ConfigurationViewModel.prototype._toViewModelDictionary = function (items) {
        /** @type {Object.<ViewModel>} */
        var result = {};
        _.each(items, function (cur) { result[cur.Id] = new ViewModel(cur); });
        return result;
    };
    Object.defineProperty(ConfigurationViewModel.prototype, "selectedAccessoryIds", {
        /** @returns {Array.<number>} */
        get: function () {
            return _.map(this.selectedAccessories(), function (cur) { return cur.id; });
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @param {Array.<ViewModel>} accessories
     * @param {string} category
     */
    ConfigurationViewModel.prototype.countCategory = function (accessories, category) {
        return _.filter(accessories, function (cur) { return cur.category === category; }).length;
    };
    /** @param {number} settingsId */
    ConfigurationViewModel.prototype.selectEngineSettings = function (settingsId) {
        //deselect all other settings, because deselection doesn't work with binding
        _.each(this._engineSettingsById, function (cur) { cur.isSelected(cur.id === settingsId); });
    };
    /** @param {string} id */
    ConfigurationViewModel.prototype.selectPaint = function (id) {
        this.selectedPaintId(id);
    };
    /** @param {string} id */
    ConfigurationViewModel.prototype.selectAccessory = function (id) {
        /** @type {boolean} */
        var isSelected = this.accessoriesById[id].isSelected();
        this.accessoriesById[id].isSelected(!isSelected);
    };
    /** @param {string} antiForgeryToken */
    ConfigurationViewModel.prototype.saveChanges = function (antiForgeryToken) {
        var selectedAccessoryIds = this.selectedAccessoryIds;
        //check for changes
        var accessoriesChanged = (this._initialAccessoryIds.length !== selectedAccessoryIds.length || _.difference(this._initialAccessoryIds, this.selectedAccessoryIds).length > 0);
        var engineSettingsChanged = this._initialEngineSettings != this.selectedEngineSettings();
        var paintChanged = this._initialPaintId != this.selectedPaintId();
        var rimsChanged = this._initialRimsId != this.selectedRimsId();
        if (!accessoriesChanged && !engineSettingsChanged && !paintChanged && !rimsChanged) {
            console.debug('no configuration changes');
            return;
        }
        // package changes
        var changedData = { __RequestVerificationToken: antiForgeryToken };
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
        }).fail(function (error) {
            console.error('failed to save configuration changes: ' + error.responseText + ' (' + error.statusText + ')');
            console.debug(JSON.stringify(error));
            //alert('something went wrong. see console for details');
        });
    };
    return ConfigurationViewModel;
}());
