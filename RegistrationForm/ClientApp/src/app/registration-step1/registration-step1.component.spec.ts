import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationStep1Component } from './registration-step1.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('RegistrationStep1Component', () => {
  let component: RegistrationStep1Component;
  let fixture: ComponentFixture<RegistrationStep1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrationStep1Component ],
      imports: [FormsModule, ReactiveFormsModule, HttpClientModule, Router]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationStep1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
