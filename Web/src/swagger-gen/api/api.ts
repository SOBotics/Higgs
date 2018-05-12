export * from './admin.service';
import { AdminService } from './admin.service';
export * from './authentication.service';
import { AuthenticationService } from './authentication.service';
export * from './bot.service';
import { BotService } from './bot.service';
export * from './file.service';
import { FileService } from './file.service';
export * from './reviewer.service';
import { ReviewerService } from './reviewer.service';
export const APIS = [AdminService, AuthenticationService, BotService, FileService, ReviewerService];
