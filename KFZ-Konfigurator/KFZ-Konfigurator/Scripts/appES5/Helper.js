'use strict';
/**
 * @param {string} url
 * @param {string} id
 * @param {string} antiForgeryToken
 * @param {Object} [additionalData]
 * @returns {jqXHR}
 */
function saveViewModel(url, id, antiForgeryToken, additionalData) {
    var i;
    /** @type {Object} */
    var dataObject = {
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
