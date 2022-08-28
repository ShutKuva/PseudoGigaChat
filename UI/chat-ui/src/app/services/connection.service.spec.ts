import { TestBed } from '@angular/core/testing';

import { SignalConnectionService } from './signalr-connection.service';

describe('AuthService', () => {
  let service: SignalConnectionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SignalConnectionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
