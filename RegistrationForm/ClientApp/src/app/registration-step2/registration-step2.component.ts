import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { WizardService } from '../services/wizard.service';
import { Router } from '@angular/router';
import { AccountWithPassword } from '../models/accountWithPassword';
import { Country } from '../models/Country';
import { Province } from '../models/province';
import { CountriesService } from '../services/countries.service';
import { AccountsService } from '../services/accounts.service';
import { ProvincesService } from '../services/provinces.service';

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
  newProvince: Province;


  constructor(
    private formBuilder: FormBuilder,
    private wizardService: WizardService,
    private router: Router,
    private countriesService: CountriesService,
    private provincesService: ProvincesService,
    private accountsService: AccountsService,
  ) { }

  ngOnInit() {
    this.account = new AccountWithPassword();

    // let account = this.wizardService.getAccount();
    // if(!account){
    //   this.router.navigate(['/step1']);
    // }

    this.form = this.formBuilder.group({
      'country': new FormControl(this.selectedCountry, [Validators.required]),
      'provinceControl': new FormControl(this.newProvince, [Validators.required])
    });

    this.getCountries();
 
    this.country.valueChanges.subscribe((country: Country) => {
      this.getProvinces(country);
    })

    this.province.valueChanges.subscribe((province: Province) => {
      console.log(JSON.stringify(province));
      console.log(JSON.stringify(this.selectedProvince));
      console.log(JSON.stringify(this.newProvince));
      // this.selectedProvince = province;
      // console.log(JSON.stringify(this.selectedProvince));
      // console.log(JSON.stringify(self.selectedProvince));
    })
  }

  get country() { return this.form.get('country'); }
  get province() { return this.form.get('provinceControl'); }

  getCountries(): void {
    this.countriesService.getCountries()
      .subscribe(countries => this.countryList = countries);
  }

  getProvinces(country: Country): void {
    this.provincesService.getProvince(country.countryId)
      .subscribe(provinces => {
        this.provinceList = provinces;
        //this.selectedProvince = provinces[0];
      });
  }

  saveAccount(account: AccountWithPassword){
    this.accountsService.addAccount(account)
      .subscribe(
        () => this.router.navigate(['/accountsList']), 
        error => {
          alert(JSON.stringify(error));
          this.router.navigate(['/step1']);
        });
  }

  onSubmit() {
    if (this.form.valid) {
      this.account.provinceId = this.selectedProvince.provinceId;
      this.saveAccount(this.account);
      this.wizardService.cleanUp();
    }
  }

}
