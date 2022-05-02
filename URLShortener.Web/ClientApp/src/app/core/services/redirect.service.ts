import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router } from "@angular/router";
import { Observable } from "rxjs";
import { UrlShortenerService } from "./url-shortener.service";

@Injectable({
  providedIn: 'root'
})
export class RedirectService implements CanActivate {

  constructor(private router:Router, private urlShortenerService: UrlShortenerService) { }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> | Promise<boolean>  {
   
    return new Promise((resolve, reject) => {

      if (route.params['shortCode'] == null)
        resolve(false);

      this.urlShortenerService.getProvidedURL(route.params['shortCode']).subscribe({
        next: result => {
          var provided_url = result;
          // Record visit
          this.urlShortenerService.updateVisit(route.params['shortCode']).subscribe({
            next: result => {
              // Redirect to providedURL
              window.location.href = provided_url;
              resolve(true);
            },
            error: err => alert(err.message)
          });

         
        },
        error: err => {
          this.router.navigate(['/']);
          resolve(false);
        }
      });

    });
   
 
  }

 
}
