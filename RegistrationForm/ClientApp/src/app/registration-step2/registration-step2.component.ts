import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { WizardService } from '../wizard.service';
import { PasswordComplexityCheckerService } from '../password-complexity-checker.service';
import { Router } from '@angular/router';
import { AccountWithPassword } from '../models/accountWithPassword';

@Component({
  selector: 'app-registration-step2',
  templateUrl: './registration-step2.component.html',
  styleUrls: ['./registration-step2.component.css']
})
export class RegistrationStep2Component implements OnInit {

  account: AccountWithPassword;

  constructor(
    private formBuilder: FormBuilder,
    private wizardService: WizardService,
    private passwordComplexityChecker: PasswordComplexityCheckerService,
    private router: Router
  ) { }

  ngOnInit() {
    let account = this.wizardService.getAccount();
    if(!account){
      this.router.navigate(['/step1']);
    }
  }

}
