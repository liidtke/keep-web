import { Service } from './service';
import { writable, derived } from "svelte/store";
import { AsideType } from "./types";

export const service = writable(new Service());
export const isOpen = writable(false);
export const currentAside = writable(AsideType.Student)

export const localities = writable([]);

