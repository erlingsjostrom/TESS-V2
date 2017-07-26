import { selector } from 'rxjs/operator/multicast';
import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, NavigationEnd, Params, PRIMARY_OUTLET } from "@angular/router";
import { BreadcrumbService } from "./breadcrumb.service";

@Component({
    selector: "breadcrumb",
    templateUrl: "breadcrumb.component.html",
    styleUrls: ['./breadcrumb.component.scss'],
})

export class BreadcrumbComponent implements OnInit {
    
    public activePageTitle: string;
    //The breadcrumbs of the current route
    public currentBreadcrumbs: IBreadcrumb[];
    //All the breadcrumbs
    public breadcrumbs: IBreadcrumb[];


    constructor(private _breadcrumbService: BreadcrumbService, private _activatedRoute: ActivatedRoute, private _router: Router) {
        _breadcrumbService.get().subscribe((breadcrumbs: IBreadcrumb[]) => {
            this.breadcrumbs = breadcrumbs as IBreadcrumb[];
        });
    }

    ngOnInit() {
        //subscribe to the NavigationEnd event
        this._router.events.filter(event => event instanceof NavigationEnd).subscribe(event => {
            //reset currentBreadcrumbs
            this.currentBreadcrumbs = [];

            //get the root of the current route
            let currentRoute: ActivatedRoute = this._activatedRoute.root;

            //set the url to an empty string
            let url: string = "";

            let i = 0;
            //iterate from activated route to children
            while (currentRoute.children.length > 0) {
                let childrenRoutes: ActivatedRoute[] = currentRoute.children;
                var breadCrumbLabel: string = '';
                
                //iterate over each children
                childrenRoutes.forEach(route => {
                    // Set currentRoute to this route
                    currentRoute = route;
                    
                    // Verify this is the primary route
                    if (route.outlet !== PRIMARY_OUTLET) {
                        return;
                    }

                    // Get the route's URL segment
                    let routeURL: string = route.snapshot.url.map(segment => segment.path).join("/");
                    if(!routeURL){
                        if(i != 0){
                            return;
                        }
                    }
                    else {
                        url += '/' + routeURL;
                    }

                    /*
                     Verify the custom data property "breadcrumb"
                     is specified on the route.
                     */
                    if (route.snapshot.data.hasOwnProperty("breadcrumb")) {
                        breadCrumbLabel = route.snapshot.data["breadcrumb"];
                    }
                    else if (i == 0){
                        breadCrumbLabel = 'Home';
                    }
                    else { //fallback
                        breadCrumbLabel = 'empty_breadcrumb_name';
                    }

                    // Add breadcrumb
                    let breadcrumb: IBreadcrumb = {
                        label: breadCrumbLabel,
                        url: url
                    };
                    
                    this.currentBreadcrumbs.push(breadcrumb);
                });

                i++;
                this._breadcrumbService.store(this.currentBreadcrumbs);
            }
            this.activePageTitle = this.currentBreadcrumbs[this.currentBreadcrumbs.length - 1].label;
        });
    }
}

export interface IBreadcrumb {
    label: string,
    url: string;
}