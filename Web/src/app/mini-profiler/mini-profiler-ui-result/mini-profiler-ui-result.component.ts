import { Component, OnInit, Input, Output, EventEmitter, ElementRef, ViewChild } from '@angular/core';
import { ProfileResultChild, ProfileResult } from '../models/ProfileResult';

@Component({
  selector: 'app-mini-profiler-ui-result',
  templateUrl: './mini-profiler-ui-result.component.html',
  styleUrls: ['./mini-profiler-ui-result.component.scss']
})
export class MiniProfilerUiResultComponent implements OnInit {

  @Input()
  public result: ProfileResultChild;
  @Input()
  public active: boolean;
  @Input()
  public rootCustomTiming;
  @Input()
  public TopLevel: ProfileResult;
  @Input()
  public ProfilerWidth = 85;
  @Input()
  public Position: 'Left' | 'Right' = 'Left';

  @Output() clicked: EventEmitter<any> = new EventEmitter<any>();

  private showingMore: boolean;
  private showingTrivial: boolean;
  private numTrivial: number;

  private customTimingCategories: string[] = [];
  private descendants;

  constructor() {
  }

  public onClick() {
    for (let i = 0; i < this.descendants.length; i++) {
      const descendant = this.descendants[i];
      if (descendant.customQueryVisible) {
        return;
      }
    }
    this.clicked.next(this.result);
  }

  public onShowMoreClicked() {
    this.showingMore = !this.showingMore;
  }
  public onShowTrivialClicked() {
    this.showingTrivial = !this.showingTrivial;
  }
  public onCustomQueryClicked(child, event) {
    event.stopImmediatePropagation();
    child.customQueryVisible = true;
  }
  public onCustomQueryClickedOutside(child) {
    child.customQueryVisible = false;
  }

  public renderIndent(depth) {
    let result = '';
    for (let i = 1; i < depth; i++) {
      result += '&nbsp;';
    }
    return result;
  }
  public getDescendants() {
    return this.getDescendantsRecursively(this.result);
  }

  public getDescendantsRecursively(node) {
    const result = [];
    if (!node.Children) {
      return [];
    }
    for (let i = 0; i < node.Children.length; i++) {
      const child = node.Children[i];
      result.push(child);
      const subChildren = this.getDescendantsRecursively(child);
      for (let j = 0; j < subChildren.length; j++) {
        result.push(subChildren[j]);
      }
    }
    return result;
  }

  ngOnInit() {
    this.customTimingCategories = Object.keys(this.rootCustomTiming);
    if (!this.result.Children) {
      this.result.Children = [];
    }

    this.descendants = this.getDescendants();
    this.numTrivial = this.descendants.reduce((n, val) => n + (val.IsTrivial ? 1 : 0), 0);
    if (this.numTrivial === this.descendants.length) {
      this.showingTrivial = true;
    }
  }
}
