import { BaseEntity } from "./base-entities/base-entity";
import { UserDTOFromServer } from "./userDTO-from-server";

export interface MessageDTO extends BaseEntity{
    groupId: number;
    text: string;
    user: UserDTOFromServer;
    replied?: MessageDTO;
    created: Date;
}