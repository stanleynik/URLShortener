import { Component } from '@angular/core';
import { TokenStorageService } from './core/services/token-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';


  constructor(private tokenStorageService:TokenStorageService) { }

  isLoggedIn() {
    return this.tokenStorageService.isLoggedIn();
  }

}
