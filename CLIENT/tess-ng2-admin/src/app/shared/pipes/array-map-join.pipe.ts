import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'arrayMapJoin'})
export class ArrayMapJoinPipe implements PipeTransform {
  transform(array: object[], property: string): string {
    const result = array.map(r => r[property]).join(", ");
    return result;
  }
}