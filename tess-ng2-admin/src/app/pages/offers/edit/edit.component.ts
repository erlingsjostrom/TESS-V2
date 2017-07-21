import { ModalService } from '../../../shared/modals/modal.service';
import { EntityField } from '../../../shared/components/entity-editor';
import { Component, OnInit, DoCheck, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

@Component({
	selector: 'edit',
	templateUrl: 'edit.component.html',
	styleUrls: ['./edit.component.scss'],
})

export class EditComponent {
	

}

