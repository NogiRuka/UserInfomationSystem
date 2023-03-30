function log(...args) {
    const name = "Forms";
    const logPrefix = [
        "%c" + name,
        `background:#52c41a;border-radius: 0.5em;color: white;font-weight: bold;padding: 2px 0.5em`,
    ];
    console.log(...logPrefix, ...args);
}