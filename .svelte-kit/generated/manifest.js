const c = [
	() => import("../../src/routes/__layout.svelte"),
	() => import("../runtime/components/error.svelte"),
	() => import("../../src/routes/index.svelte"),
	() => import("../../src/routes/localidades.svelte"),
	() => import("../../src/routes/alunos/index.svelte"),
	() => import("../../src/routes/alunos/[id].svelte"),
	() => import("../../src/routes/cursos.svelte"),
	() => import("../../src/routes/sobre.svelte")
];

const d = decodeURIComponent;

export const routes = [
	// src/routes/index.svelte
	[/^\/$/, [c[0], c[2]], [c[1]]],

	// src/routes/localidades.svelte
	[/^\/localidades\/?$/, [c[0], c[3]], [c[1]]],

	// src/routes/alunos/index.svelte
	[/^\/alunos\/?$/, [c[0], c[4]], [c[1]]],

	// src/routes/alunos/[id].svelte
	[/^\/alunos\/([^/]+?)\/?$/, [c[0], c[5]], [c[1]], (m) => ({ id: d(m[1])})],

	// src/routes/cursos.svelte
	[/^\/cursos\/?$/, [c[0], c[6]], [c[1]]],

	// src/routes/sobre.svelte
	[/^\/sobre\/?$/, [c[0], c[7]], [c[1]]]
];

// we import the root layout/error components eagerly, so that
// connectivity errors after initialisation don't nuke the app
export const fallback = [c[0](), c[1]()];