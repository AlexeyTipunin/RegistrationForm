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
  
  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private wizardService: WizardService,
    private passwordComplexityChecker: PasswordComplexityCheckerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      'login': new FormControl('', [
        Validators.required,
        Validators.email
      ]),
      'password': new FormControl('', [
        Validators.required,
        RegistrationFormValidator.passwordComplexityChecker(this.passwordComplexityChecker)
      ]),
      'passwordConfirmation': new FormControl('', [ Validators.required ]),
      'agreement': new FormControl(false, [Validators.required])
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
      let account  = new AccountWithPassword();
      account.login = this.login.value;
      account.password = this.password.value;
      account.passwordConfirmation = this.passwordConfirmation.value;
      account.agreeToWorkForFood = this.agreement.value;

      this.wizardService.setAccount(account)
      this.router.navigate(['/step2']);
    }
  }
}


