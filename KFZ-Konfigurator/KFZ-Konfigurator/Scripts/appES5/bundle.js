(function () { function r(e, n, t) { function o(i, f) { if (!n[i]) {
    if (!e[i]) {
        var c = "function" == typeof require && require;
        if (!f && c)
            return c(i, !0);
        if (u)
            return u(i, !0);
        var a = new Error("Cannot find module '" + i + "'");
        throw a.code = "MODULE_NOT_FOUND", a;
    }
    var p = n[i] = { exports: {} };
    e[i][0].call(p.exports, function (r) { var n = e[i][1][r]; return o(n || r); }, p, p.exports, r, e, n, t);
} return n[i].exports; } for (var u = "function" == typeof require && require, i = 0; i < t.length; i++)
    o(t[i]); return o; } return r; })()({ 1: [function (require, module, exports) {
            "use strict";
            Object.defineProperty(exports, "__esModule", {
                value: true
            });
            exports.cow = exports.cat = exports.dog = void 0;
            var dog = {
                talk: function talk() {
                    console.log('bark');
                }
            };
            exports.dog = dog;
            var cat = {
                talk: function talk() {
                    console.log('meow');
                }
            };
            exports.cat = cat;
            var cow = {
                talk: function talk() {
                    console.log('muuh');
                }
            };
            exports.cow = cow;
        }, {}], 2: [function (require, module, exports) {
            "use strict";
            var _animals = require("./animals.js");
            function test() {
                _animals.cat.talk();
                _animals.dog.talk();
            }
            function test2() {
                console.log('main test2');
            }
        }, { "./animals.js": 1 }] }, {}, [2]);
