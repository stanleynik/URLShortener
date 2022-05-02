import { Component } from '@angular/core';
import { TokenStorageService } from '../core/services/token-storage.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private tokenStorageService: TokenStorageService) {

  }

  getUsername() {
    if (this.tokenStorageService.getToken()) {
      return this.tokenStorageService.getUser().username;
    }
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout(): void {
    this.tokenStorageService.signOut();
    window.location.reload();
  }

}
