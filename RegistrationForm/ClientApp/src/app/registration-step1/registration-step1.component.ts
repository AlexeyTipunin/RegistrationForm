import { Component, OnInit } from '@angular/core';
import { WizardService } from '../services/wizard.service';
import { AccountWithPassword } from '../models/accountWithPassword';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { PasswordComplexityCheckerService } from '../services/password-complexity-checker.service';
import { RegistrationFormValidator } from "../misc/RegistrationFormValidator";
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-step1',
  templateUrl: './registration-step1.component.html',
  styleUrls: ['./registration-step1.component.css']
})
export class RegistrationStep1Component implements OnInit {

  account: AccountWithPassword;
  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private wizardService: WizardService,
    private passwordComplexityChecker: PasswordComplexityCheckerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.account = new AccountWithPassword();
    this.form = this.formBuilder.group({
      'login': new FormControl(this.account.login, [
        Validators.required,
        Validators.email
      ]),
      'password': new FormControl(this.account.login, [
        Validators.required,
        RegistrationFormValidator.passwordComplexityChecker(this.passwordComplexityChecker)
      ]),
      'passwordConfirmation': new FormControl(this.account.login, [ Validators.required ]),
      'agreement': new FormControl(this.account.login, [Validators.required])
    }, {
      validator: RegistrationFormValidator.MatchPassword
    });
  }

  get login() { return this.form.get('login'); }
  get password() { return this.form.get('password'); }
  get passwordConfirmation() { return this.form.get('passwordConfirmation'); }
  get agreement() { return this.form.get('agreement'); }

  onSubmit() {
    if(this.form.valid)
    {
      this.wizardService.setAccount(this.account)
      this.router.navigate(['/step2']);
    }
  }
}


