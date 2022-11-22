interface ISelf {
  delay: (fn: () => void, t: number) => ISelf,
  cancel: () => ISelf
}

interface IQueue {
  fn: () => void;
  t: number
}

export const delay = (fn: () => void, t: number) => {
  var queue: IQueue[] = [], self: ISelf, timer: NodeJS.Timeout | null;

  const schedule = (fn: () => void, t: number) => {
    timer = setTimeout(() => {
      timer = null;
      fn();
      if (queue.length) {
        var item = queue.shift();
        item && schedule(item.fn, item.t);
      }
    }, t)
  }

  self = {
    delay: (fn: () => void, t: number) => {
      if (queue.length || timer) {
        queue.push({ fn: fn, t: t });
      } else {
        schedule(fn, t);
      }
      return self;
    },
    cancel: () => {
      timer && clearTimeout(timer);
      queue = [];
      return self;
    }
  };

  return self.delay(fn, t);
}