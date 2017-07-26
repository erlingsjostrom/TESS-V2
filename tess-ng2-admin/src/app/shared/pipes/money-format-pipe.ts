import {Pipe} from "@angular/core";
import {PipeTransform} from "@angular/core";

@Pipe({name: 'moneyFormat'})
export class MoneyFormatPipe implements PipeTransform {

    transform(value:any, type: string) {
        const _type = type ? type : "SE";

        if(_type.match(/se/i)) {
            return this.formatSE(value);
        } else {
            throw new TypeError('Invalid currency format type: ' + _type);
        }
    }
    
    private formatSE(n) {
        if (typeof n === typeof "" && n.indexOf('%') >= 0)
            return n;
        else if (String(n).indexOf('.') > -1)
            return parseFloat(n).toFixed(2) + " kr";
        else {
            n = parseFloat(n);
            return n.toFixed(2).replace(/./g, function (c, i, a) {
                return i && c !== "." && ((a.length - i) % 3 === 0) ? ' ' + c : c;
            }) + " kr";
        }
    }

}