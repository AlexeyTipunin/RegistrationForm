import { TestBed, inject } from '@angular/core/testing';

import { PasswordComplexityCheckerService } from './password-complexity-checker.service';

describe('PasswordComplexityCheckerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PasswordComplexityCheckerService]
    });
  });

  it('should be created', inject([PasswordComplexityCheckerService], (service: PasswordComplexityCheckerService) => {
    expect(service).toBeTruthy();
  }));

  it('should be contais munber and letter', inject([PasswordComplexityCheckerService], (service: PasswordComplexityCheckerService) => {
    expect(service.check('1a')).toBeTruthy();
  }));

  it('should be contais letter and munber', inject([PasswordComplexityCheckerService], (service: PasswordComplexityCheckerService) => {
    expect(service.check('a1')).toBeTruthy();
  }));

  it('should fail with out numbers', inject([PasswordComplexityCheckerService], (service: PasswordComplexityCheckerService) => {
    expect(service.check('somepassword')).toBeFalsy();
  }));

  it('should fail with out letters', inject([PasswordComplexityCheckerService], (service: PasswordComplexityCheckerService) => {
    expect(service.check('0123456789')).toBeFalsy();
  }));
});
