import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UrlShortenerService } from '../core/services/url-shortener.service';
import { IUrl } from '../shared/models/url';
 
@Component({
  selector: 'app-top-20',
  templateUrl: './top-20.component.html'
})
export class Top20Component implements OnInit, OnDestroy {

  urls: IUrl[] = [];
  sub!: Subscription;

  constructor(private UrlShortenerService: UrlShortenerService) {
   
  }

  ngOnInit() {
   this.sub = this.UrlShortenerService.getTop20().subscribe({
      next: urls => {
       this.urls = urls;
   
      },
      error: err => alert(err)
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }


}

 
