import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthProviderService } from '../services/auth-provider.service';
import { TokenStorageService } from '../services/token-storage.service';
 
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl('')
  });
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];
  submitted = false;

  constructor(private fb: FormBuilder, private authService: AuthProviderService, private tokenStorage: TokenStorageService, private router: Router) {
   
  }
 
  ngOnInit(): void {
  
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
    }
  }

  // f to access form controls (form.controls) 
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }


  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }
    const val = this.form.value;
    this.submitted = true;
    this.authService.login(val.username, val.password).subscribe(
      data => {
        this.tokenStorage.saveToken(data.token);
        this.tokenStorage.saveUser(data.user);
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.router.navigateByUrl('/');
      },
      err => {
        if (err.status == 401)
          this.errorMessage = "Invalid username or password.";
        this.isLoginFailed = true;
      }
    );
  }

 
}
