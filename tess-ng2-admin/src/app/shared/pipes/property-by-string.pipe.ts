import {Pipe} from "@angular/core";
import {PipeTransform} from "@angular/core";

@Pipe({name: 'propertyByString'})
export class PropertyByStringPipe implements PipeTransform {

  transform(obj: Object, propertyName: string) {
    return this._byString(obj, propertyName);
  }

  private _byString(obj: Object, propertyName: string)
  {
    propertyName = propertyName.replace(/\[(\w+)\]/g, '.$1'); // convert indexes to properties
    propertyName = propertyName.replace(/^\./, '');           // strip a leading dot
    var a = propertyName.split('.');
    for (var i = 0, n = a.length; i < n; ++i) {
        var k = a[i];
        if (k in obj) {
            obj = obj[k];
        } else {
            return;
        }
    }
    return obj;
  }
}
