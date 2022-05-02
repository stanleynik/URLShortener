
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { TokenStorageService } from './services/token-storage.service';

@Injectable()
export class AuthGuard implements CanActivate {
 
  constructor(private router: Router,
    private tokenStorageService: TokenStorageService) { }

  canActivate() {
    if (!this.tokenStorageService.getToken()) {
      this.router.navigate(['login']);
    }
 
    return true;
  }

}
