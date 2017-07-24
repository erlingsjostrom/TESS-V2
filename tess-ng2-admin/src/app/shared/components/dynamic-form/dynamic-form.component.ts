import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'dynamic-form',
  styleUrls: ['dynamic-form.component.scss'],
  template: `
    <form
      class="dynamic-form"
      [formGroup]="form">
    </form>
  `
})
export class DynamicFormComponent implements OnInit {
  @Input()
  config: any[] = [];

  form: FormGroup;

  constructor(
    private _fb: FormBuilder
  ) {}

  ngOnInit() {
    this.form = this.createGroup();
  }

  createGroup() {
    const group = this._fb.group({});
    this.config.forEach(control => group.addControl(control.name, this._fb.control()));
    return group;
  }
}
export abstract class BaseInputConfiguration {
  // type: 'checkbox' | 'date' | 'radiobutton' | 'richtext' | 'select' | 'text';
  label: string;
  name: string;
  placeholder: string;
}

export class TextInputConfiguration extends BaseInputConfiguration {
  type: 'text'
}

export class CheckboxesInputConfiguration extends BaseInputConfiguration {
  type: 'checkboxes';
  options: CheckDataOption[]
}

export interface CheckDataOption {
	name: string,
	data: CheckData[]
}

export interface CheckData {
  checked: boolean,
  value: any
}