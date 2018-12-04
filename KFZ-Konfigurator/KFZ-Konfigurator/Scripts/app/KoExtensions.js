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

/** taken from https://stackoverflow.com/questions/16479637/knockout-cleannode-removes-jquery-event-bindings */
ko.utils.domNodeDisposal.cleanExternalData = function () {
    // Do nothing. Now any jQuery data associated with elements will
    // not be cleaned up when the elements are removed from the DOM.
};