import { TestBed, inject } from '@angular/core/testing';

import { ProvincesService } from './provinces.service';
import { HttpClientModule } from '@angular/common/http';

describe('ProvincesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [ProvincesService]
    });
  });

  it('should be created', inject([ProvincesService], (service: ProvincesService) => {
    expect(service).toBeTruthy();
  }));
});
