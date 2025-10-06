import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesCreate } from './sales-create';

describe('SalesCreate', () => {
  let component: SalesCreate;
  let fixture: ComponentFixture<SalesCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SalesCreate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalesCreate);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
