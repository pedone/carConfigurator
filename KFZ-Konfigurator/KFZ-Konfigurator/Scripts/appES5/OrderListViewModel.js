'use strict';
var OrderItemViewModel = /** @class */ (function () {
    /** @param {OrderData} data */
    function OrderItemViewModel(data) {
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
    return OrderItemViewModel;
}());
var OrderListViewModel = /** @class */ (function () {
    /**
     * @param {Array.<OrderData>} data
     * @param {number} pageCount
     */
    function OrderListViewModel(data, pageCount) {
        var _this = this;
        var self = this;
        /** @type {KnockoutObservableArrayStatic} */
        this.orders = ko.observableArray(_.map(data, function (cur) { return new OrderItemViewModel(cur); }));
        /**
         * koObservableArray
         * @type {Array.<number>}
         */
        this.pages = ko.observableArray([]);
        _.times(pageCount, function (index) { return _this.pages().push(index + 1); });
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
            var item = _.find(self.orders(), function (cur) { return cur.id == itemId; });
            if (!item) {
                console.error('order ' + itemId + ' not found');
                return;
            }
            /** @type {string} */
            var antiForgeryToken = getAntiForgeryToken(document);
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
                    var curPageNumber = self.currentPageIndex() + 1;
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
            var targetIndex = number - 1;
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
                self.orders(_.map(data, function (cur) { return new OrderItemViewModel(cur); }));
            }).fail(function (error) {
                console.error('failed to load page: ' + error.responseText + ' (' + error.statusText + ')');
                console.debug(JSON.stringify(error));
            });
        };
    }
    return OrderListViewModel;
}());
