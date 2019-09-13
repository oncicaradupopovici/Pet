export const groupBySimple = (list, keySelector) => list.reduce((accumulator, currentValue) => {
    (accumulator[keySelector(currentValue)] = accumulator[keySelector(currentValue)] || []).push(currentValue);
    return accumulator;
}, {});

export const groupBy = (list, keySelector, keyComparer) => list.reduce((accumulator, currentValue) => {
    const indexOf = accumulator.findIndex(element => keyComparer(element.key, keySelector(currentValue)));
    indexOf === -1
        ? accumulator.push({ key: keySelector(currentValue), value: [currentValue] })
        : accumulator[indexOf].value.push(currentValue);
    return accumulator
}, []);