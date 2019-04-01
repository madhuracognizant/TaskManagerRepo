import { TestBed } from '@angular/core/testing';

import { TasksService } from './tasks.service';
import { HttpModule } from '@angular/http';

describe('TasksService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [TasksService],
    imports : [HttpModule]

  }));

  it('should be created', () => {
    const service: TasksService = TestBed.get(TasksService);
    expect(service).toBeTruthy();
  });
});
