export const dog = {
    talk: function () {
        console.log('bark');
    }
}

export const cat = {
    talk: function () {
        console.log('meow');
    }
}

export const cow = {
    talk: function () {
        console.log('muuh');
        animalTest2();
    }
}

function animalTest2() {
    console.log('animalTest');
}

function animalTest() {
    console.log('animalTest');
}

console.log('animals loaded');