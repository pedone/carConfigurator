'use strict';
var ModelSelectionViewModel = /** @class */ (function () {
    function ModelSelectionViewModel() {
        /**
         * koObservable
         * @type {string}
         */
        this.curModelFilter = ko.observable('');
    }
    /**
     * @param {string} filter
     */
    ModelSelectionViewModel.prototype.filterModels = function (filter) {
        if (filter !== this.curModelFilter()) {
            this.curModelFilter(filter);
        }
    };
    return ModelSelectionViewModel;
}());
