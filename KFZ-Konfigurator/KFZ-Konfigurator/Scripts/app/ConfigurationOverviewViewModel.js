'use strict';

class ConfigurationOverviewViewModel {
    /** @param {HandlerOptions} options */
    constructor(options) {
        let self = this;
        /** @type {string} */
        this.configurationDescription = ko.observable("");
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
                    console.debug(error);
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