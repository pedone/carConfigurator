(function(f){if(typeof exports==="object"&&typeof module!=="undefined"){module.exports=f()}else if(typeof define==="function"&&define.amd){define([],f)}else{var g;if(typeof window!=="undefined"){g=window}else if(typeof global!=="undefined"){g=global}else if(typeof self!=="undefined"){g=self}else{g=this}g.main = f()}})(function(){var define,module,exports;return (function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
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
    animalTest2();
  }
};
exports.cow = cow;

function animalTest2() {
  console.log('animalTest');
}

function animalTest() {
  console.log('animalTest');
}

console.log('animals loaded');

},{}],2:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.test2 = test2;

var _animals = require("./animals.js");

function test() {
  _animals.cat.talk();

  _animals.dog.talk();
}

function test2() {
  console.log('main test2');
}

console.log('loaded');

},{"./animals.js":1}],3:[function(require,module,exports){
'use strict';

var _animals = require("./animals.js");

function test() {
  _animals.dog.talk();
}

console.log('loaded main2');

},{"./animals.js":1}]},{},[2,3])(3)
});
