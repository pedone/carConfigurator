'use strict';

import { dog, cat } from './animals.js'

function test() {
    cat.talk();
    dog.talk();
}

export function test2() {
    console.log('main test2');
}

console.log('loaded');