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
                value: this._toViewModelDictionary(data.engineSettings),
                writable: false,
                enumerable: false
            },
            _rimsById: {
                value: this._toViewModelDictionary(data.rims),
                writable: false,
                enumerable: false
            },
            _paintById: {
                value: this._toViewModelDictionary(data.paints),
                writable: false,
                enumerable: false
            },
            accessoriesById: {
                value: this._toViewModelDictionary(data.accessories),
                writable: false,
                enumerable: true
            }
        });

        /**
         * koObservable
         * @type {string}
         */
        this.selectedPaintId = ko.observable(this._getInitialSelectedId(data.paints));

        /**
         * koObservable
         * @type {string}
         */
        this.selectedRimsId = ko.observable(this._getInitialSelectedId(data.rims));

        /**
         * koObservable
         * @type {Array.<ViewModel>}
         */
        this.selectedAccessories = data.selectedAccessories ? ko.observableArray(_.map(data.selectedAccessories, (cur) => new ViewModel(cur))) :
            ko.observableArray(_.values(this.accessoriesById)).extend({ filterSelected: null });

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedPaint = ko.computed(() => data.selectedPaint ? new ViewModel(data.selectedPaint) : this._paintById[this.selectedPaintId()]);

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedRims = ko.computed(() => data.selectedRims ? new ViewModel(data.selectedRims) : this._rimsById[this.selectedRimsId()]);

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedEngineSettings = ko.computed(() => {
            if (data.selectedEngineSetting) {
                return new ViewModel(data.selectedEngineSetting);
            }

            let values = _.values(this._engineSettingsById);
            return _.first(_.filter(values, (cur) => cur.isSelected()));
        });

        /**
         * koComputed
         * @type {boolean}
         */
        this.isAccessoryLimitReached = ko.computed(() => {
            /** @type {number} */
            const accessoryLimit = 5;
            return this.selectedAccessories().length >= accessoryLimit;
        });

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

        if (isSelected || !this.isAccessoryLimitReached()) {
            this.accessoriesById[id].isSelected(!isSelected);
        }
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
 * @property {Array.<ViewModelData>} engineSettings
 * @property {Array.<ViewModelData>} accessories
 * @property {Array.<ViewModelData>} paints
 * @property {Array.<ViewModelData>} selectedAccessories
 * @property {ViewModelData} selectedEngineSetting
 * @property {ViewModelData} selectedPaint
 * @property {ViewModelData} selectedRims
 */
