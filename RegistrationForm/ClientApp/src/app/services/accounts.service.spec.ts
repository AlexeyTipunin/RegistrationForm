import { TestBed, inject } from '@angular/core/testing';

import { AccountsService } from './accounts.service';
import { HttpClientModule } from '@angular/common/http';

describe('AccountsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [AccountsService]
    });
  });

  it('should be created', inject([AccountsService], (service: AccountsService) => {
    expect(service).toBeTruthy();
  }));
});
