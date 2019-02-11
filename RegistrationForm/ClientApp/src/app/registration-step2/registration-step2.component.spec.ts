import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationStep2Component } from './registration-step2.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

describe('RegistrationStep2Component', () => {
  let component: RegistrationStep2Component;
  let fixture: ComponentFixture<RegistrationStep2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrationStep2Component ],
      imports: [FormsModule, ReactiveFormsModule, HttpClientModule, Router]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationStep2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
