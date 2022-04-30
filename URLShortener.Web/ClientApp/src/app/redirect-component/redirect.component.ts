import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlShortenerService } from '../services/url-shortener.service';

@Component({
  selector: 'app-redirect-component',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css']
})
export class RedirectComponent implements OnInit {

  constructor(private route: ActivatedRoute, private urlShortenerService: UrlShortenerService) {
    this.route.params.subscribe((params) => {
      alert(params);
      this.urlShortenerService.getProvidedURL(params[0]).subscribe({
        next: result => {
          window.location.href = result;
        },
        error: err => alert(err.message)
      });
  
    });
  }
  ngOnInit(): void {
  }

}
