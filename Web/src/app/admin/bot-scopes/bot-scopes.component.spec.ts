import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BotScopesComponent } from './bot-scopes.component';

describe('BotScopesComponent', () => {
  let component: BotScopesComponent;
  let fixture: ComponentFixture<BotScopesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BotScopesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BotScopesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
