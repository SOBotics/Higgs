import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BotScopeComponent } from './bot-scope.component';

describe('BotScopeComponent', () => {
  let component: BotScopeComponent;
  let fixture: ComponentFixture<BotScopeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BotScopeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BotScopeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
