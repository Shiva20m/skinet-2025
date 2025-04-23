import { Component, inject } from '@angular/core';
import {MatButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon'
import {MatBadge} from '@angular/material/badge'
import { RouterLink, RouterLinkActive } from '@angular/router';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { BusyService } from '../../core/services/busy.service';

@Component({
  selector: 'app-header',
  imports: [MatIcon, MatButton, MatBadge, RouterLink, RouterLinkActive, MatProgressBarModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  busyService = inject(BusyService);

}
