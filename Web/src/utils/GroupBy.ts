// https://stackoverflow.com/a/34890276/563532
export function GroupBy(xs: any, key: any) {
    return xs.reduce(function (rv, x) {
        (rv[x[key]] = rv[x[key]] || []).push(x);
        return rv;
    }, {});
}
