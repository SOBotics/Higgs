import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BotFeedbackTypesComponent } from './bot-feedback-types.component';

describe('BotFeedbackTypesComponent', () => {
  let component: BotFeedbackTypesComponent;
  let fixture: ComponentFixture<BotFeedbackTypesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BotFeedbackTypesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BotFeedbackTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
