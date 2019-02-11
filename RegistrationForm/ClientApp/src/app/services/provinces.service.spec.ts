import { TestBed, inject } from '@angular/core/testing';

import { ProvincesService } from './provinces.service';

describe('ProvincesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProvincesService]
    });
  });

  it('should be created', inject([ProvincesService], (service: ProvincesService) => {
    expect(service).toBeTruthy();
  }));
});
