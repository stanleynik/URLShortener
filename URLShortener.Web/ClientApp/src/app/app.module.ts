import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { UrlShortenerComponent } from './url-shortener/url-shortener.component';
import { Top20Component } from './top-20/top-20.component';
import { RedirectService } from './services/redirect.service';
import { authInterceptorProviders } from './interceptors/auth-interceptor';
import { AuthGuard } from './auth.guard';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    HomeComponent,
    UrlShortenerComponent,
    Top20Component
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, canActivate: [AuthGuard], pathMatch: "full" }, 
      { path: 'top-20', component: Top20Component, canActivate: [AuthGuard], pathMatch: "full" },
      { path: "login", component: LoginComponent },
      // Order cares 
      { path: ':shortCode', canActivate: [RedirectService], children: [] },
    ], { enableTracing: false })
  ],
  providers: [authInterceptorProviders, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
