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
 */
class ConfigurationViewModel {
    /** @param {string} viewKind */
    constructor(viewKind) {
        Object.defineProperties(this, {
            _engineSettingsById: {
                value: {},
                writable: true,
                enumerable: false
            },
            _rimsById: {
                value: {},
                writable: true,
                enumerable: false
            },
            _paintById: {
                value: {},
                writable: true,
                enumerable: false
            },
            accessoriesById: {
                value: {},
                writable: true,
                enumerable: true
            },
            _isInitialized: {
                value: false,
                writable: true,
                enumerable: false
            }
        });

        // set default values for everything, so the view bindings don't complain

        /**
         * koObservable
         * @type {string}
         */
        this.selectedPaintId = ko.observable();

        /**
         * koObservable
         * @type {string}
         */
        this.selectedRimsId = ko.observable();

        /**
         * koObservableArray
         * @type {Array.<ViewModel>}
         */
        this.selectedAccessories = ko.observableArray();

        let defaultComputed = () => ko.computed(() => { return {}; });
        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedPaint = defaultComputed();

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedRims = defaultComputed();

        /**
         * koComputed
         * @type {ViewModel}
         */
        this.selectedEngineSettings = defaultComputed();

        /**
         * koComputed
         * @type {boolean}
         */
        this.isAccessoryLimitReached = defaultComputed();

        /**
         * Calculates the combined price of everything but the engine
         * koComputed
         * @type {number}
         */
        this.extrasPrice = defaultComputed();

        /**
         * Just the engine price
         * koComputed
         * @type {number}
         */
        this.basePrice = defaultComputed();

        /**
         * The base price combined with the extras price
         * koComputed
         * @type {number}
         */
        this.fullPrice = defaultComputed();

        /**
         * koObservable
         * @type {string}
         */
        this.modelName = ko.observable();

        /**
         * koObservable
         * @type {VIEW_KIND}
         */
        this.currentViewKind = ko.observable(viewKind);

        /** @type {ConfigurationLinks} */
        this.links = {};

        /** @type {Function} */
        this.replaceConfigurationContent = () => { };
    }

    /** @param {ConfigurationData} data */
    init(data) {
        if (this._isInitialized) {
            console.error('configurationViewModel is already initialized');
            return;
        }

        this._engineSettingsById = this._toViewModelDictionary(data.engineSettings);
        this._rimsById = this._toViewModelDictionary(data.rims);
        this._paintById = this._toViewModelDictionary(data.paints);
        this.accessoriesById = this._toViewModelDictionary(data.accessories);

        this.selectedPaintId = ko.observable(this._getInitialSelectedId(data.paints));
        this.selectedRimsId = ko.observable(this._getInitialSelectedId(data.rims));
        this.selectedAccessories = ko.observableArray(_.values(this.accessoriesById)).extend({ filterSelected: null });
        this.selectedPaint = ko.computed(() => this._paintById[this.selectedPaintId()]);
        this.selectedRims = ko.computed(() => this._rimsById[this.selectedRimsId()]);
        this.selectedEngineSettings = ko.computed(() => {
            let values = _.values(this._engineSettingsById);
            return _.first(_.filter(values, (cur) => cur.isSelected()));
        });
        this.isAccessoryLimitReached = ko.computed(() => {
            /** @type {number} */
            const accessoryLimit = 5;
            return this.selectedAccessories().length >= accessoryLimit;
        });
        this.extrasPrice = ko.computed(() => {
            /** @type {number} */
            let accessoriesPrice = _.reduce(this.selectedAccessories(), (memo, cur) => memo + cur.price, 0);
            return accessoriesPrice + (this.selectedPaint() && this.selectedPaint().price) + (this.selectedRims() && this.selectedRims().price);
        });
        this.basePrice = ko.computed(() => {
            return (this.selectedEngineSettings() && this.selectedEngineSettings().price) || 0;
        });
        this.fullPrice = ko.computed(() => {
            return this.basePrice() + this.extrasPrice();
        });
        this.modelName = ko.observable(data.modelName);
        this.links = data.links;
        this.replaceConfigurationContent = data.replaceConfigurationContent;

        this._isInitialized = true;
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

    /** @returns {boolean} */
    get isInitialized() {
        return this._isInitialized;
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

    /**
     * @param {string} visibleUrl
     * @param {string} targetUrl
     * @param {string} targetViewKind
     * @param {Event} event
     * @param {boolean} reapplyViewModelContext
     */
    linkClick(visibleUrl, targetUrl, targetViewKind, event, reapplyViewModelContext) {
        event.preventDefault();

        console.debug('loading partial ' + targetUrl);
        $.get(targetUrl, (data) => {
            this.replaceConfigurationContent(data, visibleUrl, targetViewKind, reapplyViewModelContext);
        });
    }

    /** save any changes to this configuriation since the last save */
    saveChanges() {
        //TODO
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
 * @property {Array.<ViewModelData>} rims
 * @property {Array.<ViewModelData>} selectedAccessories
 * @property {string} modelName
 * @property {ConfigurationLinks} links
 * @property {Function} replaceConfigurationContent
 */

/** 
 * @typedef {Object} ConfigurationLinks
 * @property {string} engineSettingsLink
 * @property {string} engineSettingsLinkPartial
 * @property {string} accessoriesLink
 * @property {string} accessoriesLinkPartial
 * @property {string} exteriorLink
 * @property {string} exteriorLinkPartial
 * @property {string} overviewLink
 * @property {string} overviewLinkPartial
 */