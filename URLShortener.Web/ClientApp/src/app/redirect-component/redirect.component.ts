import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlShortenerService } from '../core/services/url-shortener.service';

@Component({
  selector: 'app-redirect-component',
  template: ''
})
export class RedirectComponent implements OnInit {

  constructor(private route: ActivatedRoute, private urlShortenerService: UrlShortenerService) {
    this.route.params.subscribe((params) => {
     
      this.urlShortenerService.getProvidedURL(params['shortCode']).subscribe({
        next: result => {
          // Record visit
          this.urlShortenerService.updateVisit(params['shortCode']).subscribe({
            next: result => {
             
            },
            error: err => alert(err.message)
          });

          // Redirect to providedURL
          window.location.href = result;
        },
        error: err => alert(err.message)
      });
  
    });
  }
  ngOnInit(): void {
  }

}
