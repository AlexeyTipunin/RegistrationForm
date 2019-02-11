import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule , FormGroup, FormBuilder, Validators, ValidatorFn, AbstractControl } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegistrationStep2Component } from './registration-step2/registration-step2.component';
import { RegistrationStep1Component } from './registration-step1/registration-step1.component';
import { AccountListComponent } from './account-list/account-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegistrationStep2Component,
    RegistrationStep1Component,
    AccountListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    //ValidatorFn,
    //AbstractControl,
    // FormGroup,
    // FormBuilder,
    //Validators,
    RouterModule.forRoot([
      { path: '', redirectTo: '/step1', pathMatch: 'full' },
      { path: 'step1', component: RegistrationStep1Component, pathMatch: 'full' },
      { path: 'step2', component: RegistrationStep2Component, pathMatch: 'full' },
      { path: 'accountsList', component: AccountListComponent, pathMatch: 'full' },
      // { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
