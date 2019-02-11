import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { WizardService } from '../wizard.service';
import { PasswordComplexityCheckerService } from '../password-complexity-checker.service';
import { Router } from '@angular/router';
import { AccountWithPassword } from '../models/accountWithPassword';
import { RegistrationFormValidator } from '../misc/RegistrationFormValidator';
import { Country } from '../models/Country';
import { Province } from '../models/province';

@Component({
  selector: 'app-registration-step2',
  templateUrl: './registration-step2.component.html',
  styleUrls: ['./registration-step2.component.css']
})
export class RegistrationStep2Component implements OnInit {

  account: AccountWithPassword;
  form: FormGroup;

  countryList: Country[];
  selectedCountry: Country;

  provinceList: Province[];
  selectedProvince: Province;


  constructor(
    private formBuilder: FormBuilder,
    private wizardService: WizardService,
    private passwordComplexityChecker: PasswordComplexityCheckerService,
    private router: Router
  ) { }

  ngOnInit() {
    // let account = this.wizardService.getAccount();
    // if(!account){
    //   this.router.navigate(['/step1']);
    // }

    this.form = this.formBuilder.group({
      'country': new FormControl(this.account.login, [
        Validators.required,
        // Validators.email
      ]),
      'Province': new FormControl(this.account.login, [
        Validators.required,
        RegistrationFormValidator.passwordComplexityChecker(this.passwordComplexityChecker)
      ])});
  }

  get login() { return this.form.get('login'); }
  get password() { return this.form.get('password'); }
  get passwordConfirmation() { return this.form.get('passwordConfirmation'); }
  get agreement() { return this.form.get('agreement'); }

  onSubmit() {
    if(this.form.valid)
    {
      //this.wizardService.setAccount(this.account)
      this.router.navigate(['/accountsList']);
    }
  }

}
