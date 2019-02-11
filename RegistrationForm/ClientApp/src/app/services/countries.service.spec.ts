import { TestBed, inject } from '@angular/core/testing';

import { CountriesService } from './countries.service';
import { HttpClientModule } from '@angular/common/http';

describe('CountriesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [CountriesService]
    });
  });

  it('should be created', inject([CountriesService], (service: CountriesService) => {
    expect(service).toBeTruthy();
  }));
});
