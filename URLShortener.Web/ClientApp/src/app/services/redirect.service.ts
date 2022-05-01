import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate } from "@angular/router";
import { Observable } from "rxjs";
import { UrlShortenerService } from "./url-shortener.service";

@Injectable({
  providedIn: 'root'
})
export class RedirectService implements CanActivate {

  constructor(private urlShortenerService: UrlShortenerService) { }

  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> | Promise<boolean>  {
   
    return new Promise((resolve, reject) => {

      if (route.params['shortCode'] == null)
        resolve(false);

      this.urlShortenerService.getProvidedURL(route.params['shortCode']).subscribe({
        next: result => {
          // Record visit
          this.urlShortenerService.updateVisit(route.params['shortCode']).subscribe({
            next: result => {

            },
            error: err => alert(err.message)
          });

          // Redirect to providedURL
          window.location.href = result;
          resolve(true);
        },
        error: err => {
          resolve(false);
        }
      });

    });
   
 
  }

 
}
