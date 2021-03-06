import { NgModule } from "@angular/core";
import { BreadcrumbComponent } from './breadcrumb.component';
import { RouterModule } from "@angular/router";
import { BrowserModule } from "@angular/platform-browser";
import { CommonModule } from "@angular/common";
import { BreadcrumbService } from './breadcrumb.service';


@NgModule({
    declarations: [
        BreadcrumbComponent
    ],
    providers: [
        BreadcrumbService
    ],
    imports: [
        RouterModule,
        BrowserModule,
        CommonModule
    ],
    exports: [BreadcrumbComponent]
})
export class BreadcrumbModule {
}